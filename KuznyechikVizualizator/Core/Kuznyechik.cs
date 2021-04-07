using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using KuznyechikVizualizator.Core;

namespace KuznyechikVizualizator.Core
{
    class Kuznyechik
    {
        public List<byte> masterKey;       //size = {256b;32B}
        public List<List<byte>> C;         //size = 32x{128b;16B}
        public List<List<byte>> k;         //size = 34x{128b;16B}
        public List<List<byte>> roundKeys; //size = 10x{128b;16B}
        public List<List<byte>> keyGenRounds;    //size = 96x{128b;16B}
        public List<List<byte>> cryptRounds;    //size = 28x{128b;16B}
        public List<byte> plaintext;       //size = {128b;32B}
        public List<byte> ciphertext;      //size = {128b;32B}

        public Kuznyechik(string pText, string mKey, string cText)
        {
            masterKey = toList(mKey);
            plaintext = toList(pText);
            ciphertext = toList(cText);
            constGen();
            keyGen();
            if (pText == "")
            {
                decrypt();
            } else if (cText == "")
            {
                encrypt();
            }
            else
            {
                throw new Exception();
            }
        }

        public override string ToString()
        {
            return BitConverter.ToString(ciphertext.ToArray()).Replace("-", "");
        }

        static public List<byte> toList(string x)
        {
            List<byte> ans = new List<byte>(); //check for odd string size (only supports 32 or 64 size)
            for (int i = 0; i < x.Length / 2; ++i)
            {
                ans.Add(byte.Parse(x.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
            }
            return ans;
        }

        static public List<byte> X(List<byte> v1, List<byte> v2) 
        {
            List<byte> ans = new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 16; ++i) 
            {
                ans[i] = Convert.ToByte(v1[i] ^ v2[i]);
            }
            return ans;
        }

        static public List<byte> S(List<byte> v)
        {
            List<byte> answer = new List<byte>(16){ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 16; ++i)
            {
                answer[i] = s(v[i]);
            }
            return answer;
        }

        static public List<byte> reverse_S(List<byte> v)
        {
            List<byte> answer = new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 16; ++i)
            {
                answer[i] = reverse_s(v[i]);
            }
            return answer;
        }

        static public List<byte> L(List<byte> v)
        {
            List<byte> ans = v;
            for (int i = 0; i < 16; ++i)
            {
                ans = R(ans);
            }
            return ans;
        }

        static public List<byte> reverse_L(List<byte> v)
        {
            List<byte> ans = v;
            for (int i = 0; i < 16; ++i)
            {
                ans = reverse_R(ans);
            }
            return ans;
        }

        static public List<byte> R(List<byte> v)
        {
            List<byte> ans = new List<byte>(16) {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            for (int i = 1; i < 16; ++i) 
            {
                ans[i] = v[i - 1];
            }
            ans[0] = el(v);
            return ans;
        }

        static public List<byte> reverse_R(List<byte> v)
        {
            List<byte> ans = new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> newv = v.ToList();
            for (int i = 0; i < 15; ++i)
            {
                ans[i] = v[i + 1];
                newv[i] = v[i + 1];
            }
            newv[15] = v[0];
            ans[15] = el(newv);
            return ans;
        }

        public void keyGen() 
        {
           k = new List<List<byte>>();
            for (int i = 0; i < 34; ++i)
            {
                k.Add(new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
            }

            keyGenRounds = new List<List<byte>>();
            for (int i = 0; i < 96; ++i)
            {
                keyGenRounds.Add(new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
            }

            for (int i = 0; i< 16; ++i) 
            {
                k[0][i] = masterKey[16 + i];
                k[1][i] = masterKey[i];
            }
            for (int i = 0; i < 32; i++) 
            {
                keyGenRounds[i * 3 + 0] = X(C[i], k[i + 1]);
                keyGenRounds[i * 3 + 1] = S(keyGenRounds[i * 3 + 0]);
                keyGenRounds[i * 3 + 2] = L(keyGenRounds[i * 3 + 1]);
                k[i + 2] = X(k[i], keyGenRounds[i * 3 + 2]);
           }
           repos();
        }

        public void encrypt() 
        {
            ciphertext = plaintext;
            cryptRounds = new List<List<byte>>();
            cryptRounds.Add(plaintext);

            for (int i = 0; i < 9; ++i)
            {
                cryptRounds.Add(X(ciphertext, roundKeys[i]));
                cryptRounds.Add(S(X(ciphertext, roundKeys[i])));
                 cryptRounds.Add(L(S(X(ciphertext, roundKeys[i]))));
                 ciphertext = L(S(X(ciphertext, roundKeys[i])));
            }
        
            ciphertext = X(ciphertext, roundKeys[9]);
            cryptRounds.Add(ciphertext);
        }
        public void decrypt()
        {
            plaintext = ciphertext;
            cryptRounds = new List<List<byte>>();
            cryptRounds.Add(ciphertext);

            plaintext = X(plaintext, roundKeys[9]);
            cryptRounds.Add(plaintext);
            for (int i = 0; i < 9; ++i)
            {
                cryptRounds.Add(reverse_L(plaintext));
                cryptRounds.Add(reverse_S(reverse_L(plaintext)));
                cryptRounds.Add(X(reverse_S(reverse_L(plaintext)), roundKeys[8 - i]));
                plaintext = X(reverse_S(reverse_L(plaintext)), roundKeys[8 - i]);
            }
        }

        static public byte s(byte x)
        {
            byte[] s_array = new byte[256] { 252, 238, 221, 17, 207, 110, 49, 22, 251, 196, 250, 218, 35, 197, 4, 77, 233, 119, 240, 219, 147, 46, 
                                        153, 186, 23, 54, 241, 187, 20, 205, 95, 193, 249, 24, 101, 90, 226, 92, 239, 33, 129, 28, 60, 66, 139, 1, 
                                        142, 79, 5, 132, 2, 174, 227, 106, 143, 160, 6, 11, 237, 152, 127, 212, 211, 31, 235, 52, 44, 81, 234, 200, 
                                      72, 171, 242, 42, 104, 162, 253, 58, 206, 204, 181, 112, 14, 86, 8, 12, 118, 18, 191, 114, 19, 71, 156, 183, 
                                       93, 135, 21, 161, 150, 41, 16, 123, 154, 199, 243, 145, 120, 111, 157, 158, 178, 177, 50, 117, 25, 61, 255, 
                                       53, 138, 126, 109, 84, 198, 128, 195, 189, 13, 87, 223, 245, 36, 169, 62, 168, 67, 201, 215, 121, 214, 246, 
                                       124, 34, 185, 3, 224, 15, 236, 222, 122, 148, 176, 188, 220, 232, 40, 80, 78, 51, 10, 74, 167, 151, 96, 115, 
                                      30, 0, 98, 68, 26, 184, 56, 130, 100, 159, 38, 65, 173, 69, 70, 146, 39, 94, 85, 47, 140, 163, 165, 125, 105, 
                                         213, 149, 59, 7, 88, 179, 64, 134, 172, 29, 247, 48, 55, 107, 228, 136, 217, 231, 137, 225, 27, 131, 73, 76, 
                                         63, 248, 254, 141, 83, 170, 144, 202, 216, 133, 97, 32, 113, 103, 164, 45, 43, 9, 91, 203, 155, 37, 208, 190, 
                                         229, 108, 82, 89, 166, 116, 210, 230, 244, 180, 192, 209, 102, 175, 194, 57, 75, 99, 182 };
            return s_array[x];
        }

        static public byte reverse_s(byte x)
        {
            byte[] s_array = new byte[256] { 165, 45, 50, 143, 14, 48, 56, 192, 84, 230, 158, 57, 85, 126, 82, 145, 100, 3, 87, 90, 28, 96, 7, 24,
                33, 114, 168, 209, 41, 198, 164, 63, 224, 39, 141, 12, 130, 234, 174, 180, 154, 99, 73, 229, 66, 228, 21, 183, 200, 6, 112, 157,
                65, 117, 25, 201, 170, 252, 77, 191, 42, 115, 132, 213, 195, 175, 43, 134, 167, 177, 178, 91, 70, 211, 159, 253, 212, 15, 156, 47,
                155, 67, 239, 217, 121, 182, 83, 127, 193, 240, 35, 231, 37, 94, 181, 30, 162, 223, 166, 254, 172, 34, 249, 226, 74, 188, 53, 202,
                238, 120, 5, 107, 81, 225, 89, 163, 242, 113, 86, 17, 106, 137, 148, 101, 140, 187, 119, 60, 123, 40, 171, 210, 49, 222, 196, 95, 204,
                207, 118, 44, 184, 216, 46, 54, 219, 105, 179, 20, 149, 190, 98, 161, 59, 22, 102, 233, 92, 108, 109, 173, 55, 97, 75, 185, 227, 186,
                241, 160, 133, 131, 218, 71, 197, 176, 51, 250, 150, 111, 110, 194, 246, 80, 255, 93, 169, 142, 23, 27, 151, 125, 236, 88, 247, 31, 251,
                124, 9, 13, 122, 103, 69, 135, 220, 232, 79, 29, 78, 4, 235, 248, 243, 62, 61, 189, 138, 136, 221, 205, 11, 19, 152, 2, 147, 128, 144,
                208, 36, 52, 203, 237, 244, 206, 153, 16, 68, 64, 146, 58, 1, 38, 18, 26, 72, 104, 245, 129, 139, 199, 214, 32, 10, 8, 0, 76, 215, 116 };
            return s_array[x];
        }
        static public UInt16 mul(byte a, byte b)
        {
            UInt16 ans = 0;
            for (int i = 0; i < 8; ++i)
            {
                ans = Convert.ToUInt16(ans ^ Convert.ToUInt16(a * Convert.ToUInt16(b & Convert.ToUInt16(1 << i))));
            }
            return ans;
        }

        static public byte norm(UInt16 x)
        {
            UInt16 p = 451; //por polynom
            for (int i = 15; i > 7; i--)
            {
                if ((x & (1 << i)) != 0)
                {
                    x = Convert.ToUInt16(x ^ Convert.ToUInt16(p << (i - 8)));
                }
            }
            return Convert.ToByte(x);
        }

        public void constGen()
        {
            C = new List<List<byte>>();
            for (int i = 0; i < 36; ++i)
            {
                C.Add(new List<byte>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }
            for (int i = 0; i < 32; i++)
            {
                C[i][15] = Convert.ToByte(i + 1);
                C[i] = L(C[i]);
            }
        }

        public void repos()
        {
            roundKeys = new List<List<byte>>();
            roundKeys.Add(k[1]);
            roundKeys.Add(k[0]);
            roundKeys.Add(k[9]);
            roundKeys.Add(k[8]);
            roundKeys.Add(k[17]);
            roundKeys.Add(k[16]);
            roundKeys.Add(k[25]);
            roundKeys.Add(k[24]);
            roundKeys.Add(k[33]);
            roundKeys.Add(k[32]);
        }

        static public byte el(List<byte> v)
        {
            UInt16 ans = Convert.ToUInt16(mul(148, v[0]) ^ mul(32, v[1]) ^ mul(133, v[2]) ^ mul(16, v[3]) ^ mul(194, v[4]) ^ mul(192, v[5]) ^ mul(1, v[6]) ^ mul(251, v[7]) ^ mul(1, v[8]) ^ mul(192, v[9]) ^ mul(194, v[10]) ^ mul(16, v[11]) ^ mul(133, v[12]) ^ mul(32, v[13]) ^ mul(148, v[14]) ^ mul(1, v[15]));
            return norm(ans);
        }
    };
}
