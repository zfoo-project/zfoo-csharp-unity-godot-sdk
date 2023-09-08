using System;
using System.Collections.Generic;
using CsProtocol.Buffer;

namespace CsProtocol
{
    
    public class PairLong : IProtocol
    {
        public long key;
        public long value;

        public static PairLong ValueOf(long key, long value)
        {
            var packet = new PairLong();
            packet.key = key;
            packet.value = value;
            return packet;
        }


        public short ProtocolId()
        {
            return 111;
        }
    }


    public class PairLongRegistration : IProtocolRegistration
    {
        public short ProtocolId()
        {
            return 111;
        }

        public void Write(ByteBuffer buffer, IProtocol packet)
        {
            if (buffer.WritePacketFlag(packet))
            {
                return;
            }
            PairLong message = (PairLong) packet;
            buffer.WriteLong(message.key);
            buffer.WriteLong(message.value);
        }

        public IProtocol Read(ByteBuffer buffer)
        {
            if (!buffer.ReadBool())
            {
                return null;
            }
            PairLong packet = new PairLong();
            long result0 = buffer.ReadLong();
            packet.key = result0;
            long result1 = buffer.ReadLong();
            packet.value = result1;
            return packet;
        }
    }
}
