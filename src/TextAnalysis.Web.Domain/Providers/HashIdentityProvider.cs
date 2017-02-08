using System;
using System.Text;
using HashidsNet;

public class HashIdentityProvider: IUniqueIdentifierProvider 
{
    public string Generate(string key)
    {
        var hashIds = new Hashids("text-analysis");
        
        var hash = hashIds.EncodeHex(this.GetHexFromString(key));
        
        return hash;
    }
    
    private string GetHexFromString(string value)
    {
        var b = Encoding.UTF8.GetBytes(value);
        var hexValue = BitConverter.ToString(b);
        
        return hexValue;
    }
}