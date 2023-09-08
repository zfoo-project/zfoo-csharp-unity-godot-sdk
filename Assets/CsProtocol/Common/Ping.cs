using System;
using System.Collections.Generic;
using CsProtocol.Buffer;

namespace CsProtocol
{
    
    public class Ping : IProtocol
    {
        

        public static Ping ValueOf()
        {
            var packet = new Ping();
            
            return packet;
        }


        public short ProtocolId()
        {
            return 103;
        }
    }


    public class PingRegistration : IProtocolRegistration
    {
        public short ProtocolId()
        {
            return 103;
        }

        public void Write(ByteBuffer buffer, IProtocol packet)
        {
            if (buffer.WritePacketFlag(packet))
            {
                return;
            }
            Ping message = (Ping) packet;
            
        }

        public IProtocol Read(ByteBuffer buffer)
        {
            if (!buffer.ReadBool())
            {
                return null;
            }
            Ping packet = new Ping();
            
            return packet;
        }
    }
}
