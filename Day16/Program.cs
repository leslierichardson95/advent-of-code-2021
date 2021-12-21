using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day16/input.txt").First();

            Packet packet = Packet.ParseData(new BitParser(input));
            Console.WriteLine("Part 1: " + GetSum(packet));
            Console.WriteLine("Part 2: " + packet.Value);
        }

        static int GetSum(Packet packet)
        {
            if (packet is LiteralPacket) 
                return packet.Version;
            if (packet is OperatorPacket operatorPacket) 
                return packet.Version + operatorPacket.SubPackets.Select(GetSum).Sum();
            throw new Exception("?");
        }
    }

    abstract class Packet
    {
        public int Version;
        public int TypeID;
        //public List<Packet> SubPackets;

        public Packet(int version, int typeId)
        {
            this.Version = version;
            this.TypeID = typeId;
        }

        public abstract long Value { get; }

        public static Packet ParseData(BitParser parser)
        {
            int version = parser.GetDecimalNumber(3);
            int typeId = parser.GetDecimalNumber(3);

            if (typeId == 4)
            {
                Console.WriteLine($"Version: {version}, TypeId: {typeId}, Packet: Literal");
                return LiteralPacket.Parse(parser, version);
            }
            else
            {
                Console.WriteLine($"Version: {version}, TypeId: {typeId}, Packet: Operator");
                return OperatorPacket.Parse(parser, version, typeId);
            }
        }
    }

    class LiteralPacket : Packet
    {
        private readonly long LiteralValue;

        private LiteralPacket(int version, long value) : base(version, 4)
        {
            LiteralValue = value;
        }

        public override long Value { get { return LiteralValue; } }

        public static LiteralPacket Parse(BitParser parser, int version)
        {
            long value = 0;

            bool areMoreDigits = true;
            while (areMoreDigits)
            {
                areMoreDigits = parser.GetBit();
                value <<= 4;
                value |= parser.GetDecimalNumber(4);
            }

            return new LiteralPacket(version, value);
        }
    }

    class OperatorPacket : Packet
    {
        public List<Packet> SubPackets { get; }
        private OperatorPacket(int version, int typeId, List<Packet> subPackets) : base(version, typeId)
        {
            SubPackets = subPackets;
        }

        public static OperatorPacket Parse(BitParser parser, int version, int typeId)
        {
            bool isBitAvailable = parser.GetBit();
            List<Packet> subPackets = new List<Packet>();

            if (!isBitAvailable)
            {
                int totalSize = parser.GetDecimalNumber(15);
                int limit = parser.PtrPosition + totalSize;

                while (parser.PtrPosition < limit) 
                    subPackets.Add(ParseData(parser));
            }
            else
            {
                int numberOfSidePackets = parser.GetDecimalNumber(11);
                while (subPackets.Count < numberOfSidePackets) subPackets.Add(ParseData(parser));
            }

            return new OperatorPacket(version, typeId, subPackets);
        }

        public override long Value => EvaluateOperation(TypeID, SubPackets);

        private static long EvaluateOperation(int operationId, List<Packet> subPackets)
        {
            List<long> values = subPackets.Select(x => x.Value).ToList();
            switch (operationId)
            {
                case 0: return values.Sum();
                case 1: return values.Aggregate((x, y) => x * y);
                case 2: return values.Min();
                case 3: return values.Max();
                case 5: return values.First() > values.Last() ? 1 : 0;
                case 6: return values.First() < values.Last() ? 1 : 0;
                case 7: return values.First() == values.Last() ? 1 : 0;
                default: throw new InvalidOperationException($"Unknown operator {operationId}");
            }
        }
    }

    class BitParser
    {
        public readonly string Transmission;
        public int PtrPosition { get; set; } = 0;

        public BitParser(string transmission) 
        {
            Transmission = ConvertToBits(transmission);
        }

        public bool GetBit()
        {
            //if (PtrPosition == 103) return false;
            if (Transmission[PtrPosition++] == '1') return true;
            else return false;
        }

        public string GetBits(int bitSize)
        {
            //if ((Transmission.Length - PtrPosition) < 11)
            //{
            //    bitSize = Transmission.Length - PtrPosition - 1;
            //    //PtrPosition = Transmission.Length - 1;
            //    string b = Transmission.AsSpan(PtrPosition, bitSize).ToString();
            //    return b;
            //}
            //else
            //{
                string bits = Transmission.Substring(PtrPosition, bitSize);
                PtrPosition += bitSize;
                if (bits.Equals("")) return "0";
                return bits;
            //}
            
        }

        public int GetDecimalNumber(int bitSize)
        {
            return (int)Convert.ToInt64(GetBits(bitSize), 2); 
        }

        public string ConvertToBits(string str)
        {
            Dictionary<char, string> binaryBits = new Dictionary<char, string>()
            {
                ['0'] = "0000",
                ['1'] = "0001",
                ['2'] = "0010",
                ['3'] = "0011",
                ['4'] = "0100",
                ['5'] = "0101",
                ['6'] = "0110",
                ['7'] = "0111",
                ['8'] = "1000",
                ['9'] = "1001",
                ['A'] = "1010",
                ['B'] = "1011",
                ['C'] = "1100",
                ['D'] = "1101",
                ['E'] = "1110",
                ['F'] = "1111"
            };

            string binaryTransmission = "";
            foreach (char number in str) binaryTransmission += binaryBits[number];
            return binaryTransmission;
        }
    }
}
