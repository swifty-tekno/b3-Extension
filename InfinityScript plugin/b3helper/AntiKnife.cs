using System;
using System.IO;
using InfinityScript;

namespace snipe
{
    public partial class SNIPE
    {
        private int ProcessID = System.Diagnostics.Process.GetCurrentProcess().Id;

        private int DefaultKnifeAddress;
        private unsafe int* KnifeRange;
        private unsafe int* ZeroAddress;

        public unsafe void SetupKnife()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"Knife"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"Knife");

            try
            {
                byte?[] search1 = new byte?[23]
                {
                  new byte?((byte) 139),
                  new byte?(),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 131),
                  new byte?(),
                  new byte?((byte) 4),
                  new byte?(),
                  new byte?((byte) 131),
                  new byte?(),
                  new byte?((byte) 12),
                  new byte?((byte) 217),
                  new byte?(),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 139),
                  new byte?(),
                  new byte?((byte) 217),
                  new byte?(),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 217),
                  new byte?((byte) 5)
                };
                KnifeRange = (int*)(FindMem(search1, 1, 4194304, 5242880) + search1.Length);
                if ((int)KnifeRange == search1.Length)
                {
                    byte?[] search2 = new byte?[25]
                    {
                        new byte?((byte) 139),
                        new byte?(),
                        new byte?(),
                        new byte?(),
                        new byte?((byte) 131),
                        new byte?(),
                        new byte?((byte) 24),
                        new byte?(),
                        new byte?((byte) 131),
                        new byte?(),
                        new byte?((byte) 12),
                        new byte?((byte) 217),
                        new byte?(),
                        new byte?(),
                        new byte?(),
                        new byte?((byte) 141),
                        new byte?(),
                        new byte?(),
                        new byte?(),
                        new byte?((byte) 217),
                        new byte?(),
                        new byte?(),
                        new byte?(),
                        new byte?((byte) 217),
                        new byte?((byte) 5)
                    };
                    this.KnifeRange = (int*)(FindMem(search2, 1, 4194304, 5242880) + search2.Length);
                    if ((int)this.KnifeRange == search2.Length)
                        this.KnifeRange = null;
                }
                this.DefaultKnifeAddress = *this.KnifeRange;
                byte?[] search3 = new byte?[24]
                {
                  new byte?((byte) 217),
                  new byte?((byte) 92),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 216),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 216),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 217),
                  new byte?((byte) 92),
                  new byte?(),
                  new byte?(),
                  new byte?((byte) 131),
                  new byte?(),
                  new byte?((byte) 1),
                  new byte?((byte) 15),
                  new byte?((byte) 134),
                  new byte?(),
                  new byte?((byte) 0),
                  new byte?((byte) 0),
                  new byte?((byte) 0),
                  new byte?((byte) 217)
                };
                this.ZeroAddress = (int*)(FindMem(search3, 1, 4194304, 5242880) + search3.Length + 2);

                if (!((int)KnifeRange != 0 && DefaultKnifeAddress != 0 && (int)ZeroAddress != 0))
                    Log.Error("Error finding address: NoKnife Plugin will not work");
            }
            catch (Exception ex)
            {
                Log.Error("Error in NoKnife Plugin. Plugin will not work.");
                Log.Error(ex.ToString());
            }




            //looks bad, but actually the else will always be fired "first"

            if (DefaultKnifeAddress == (int)ZeroAddress)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + @"Knife\addr_" + ProcessID))
                {
                    //    print("now it will be feked");
                    Log.Error("Error: NoKnife will not work.");
                    return;
                }

                // print("restoring proper knife addr");

                DefaultKnifeAddress = int.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + @"Knife\addr_" + ProcessID));

                //  print("done");

            }
            else
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + @"Knife\addr_" + ProcessID, DefaultKnifeAddress.ToString());     //save for when it's feked
                                                                                                                             //  print("knife def addr saved");
            }
        }

        private unsafe int FindMem(byte?[] search, int num = 1, int start = 16777216, int end = 63963136)
        {
            try
            {
                int num2 = 0;
                for (int i = start; i < end; i++)
                {
                    int num3 = i;
                    bool flag = false;
                    for (int j = 0; j < search.Length; j++)
                    {
                        if (search[j].HasValue)
                        {
                            int num4 = *(byte*)num3;
                            byte? nullable = search[j];
                            if (num4 != nullable.GetValueOrDefault() || !nullable.HasValue || 1 == 0)
                            {
                                break;
                            }
                        }
                        if (j == search.Length - 1)
                        {
                            if (num == 1)
                            {
                                flag = true;
                            }
                            else
                            {
                                num2++;
                                if (num2 == num)
                                {
                                    flag = true;
                                }
                            }
                        }
                        else
                        {
                            num3++;
                        }
                    }
                    if (flag)
                    {
                        return i;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return 0;
        }

        public unsafe void DisableKnife()
        {
            *KnifeRange = (int)ZeroAddress;
        }

        public unsafe void EnableKnife()
        {
            *KnifeRange = DefaultKnifeAddress;
        }
    }
}
