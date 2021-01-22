using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    public class ApiKeys
    {
        public static List<ApiKey> Keys { get; set; } = new List<ApiKey>
        {
            new ApiKey {  Name ="ApiAdminSecretKey", Value ="00DBEB91-9D74-4A5B-606C-08D890D16294", Roles = new[] { "Admin" } },
            new ApiKey {  Name ="ApiStandardSecretKey", Value ="4EC30DE3-21E6-41F3-A238-162A1ED7E124", Roles = new[] { "Standard" } },
            new ApiKey {  Name ="ApiTravelXAdminSecretKey", Value ="521C9B91-562D-4EE5-8E01-2FF5B361D03A", Roles = new[] { "TravelXAdmin" } }
        };

        public static ApiKey GetKey( ApiKeyName keyName )
            => GetKey( keyName.ToString() );

        public static ApiKey GetKey( string keyName )
            => Keys.FirstOrDefault( x => x.Value.Equals( keyName, StringComparison.OrdinalIgnoreCase ) );

        public static ApiKey AddNewKey( string keyName, string[] roles )
        {
            var newKey = GenerateKey( keyName, roles );

            lock ( Keys )
                Keys.Add( newKey );

            return newKey;
        }

        public static bool RemoveKey( string keyName )
        {
            var key = GetKey( keyName );
            if ( key == null || !key.CanRemove )
                return false;

            lock ( Keys )
                Keys.Remove( key );

            return true;
        }

        public static ApiKey GenerateKey( string keyName, string[] roles )
            => new ApiKey
            {
                Name = keyName,

                Value = Guid.NewGuid().ToString(),

                Roles = roles,

                CanRemove = true
            };


    }

    public enum ApiKeyName
    {
        ApiAdminSecretKey = 0,

        ApiStandardSecretKey = 1,

        ApiTravelXAdminSecretKey = 2
    }

    public class ApiKey
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string[] Roles { get; set; }

        public bool CanRemove = false;

        public string JwtValue
        {
            get
            {
                return Value.Substring( 0, 32 );
            }
        }
    }

}
