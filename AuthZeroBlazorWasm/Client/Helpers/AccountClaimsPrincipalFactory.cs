using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace AuthZeroBlazorWasm.Client.Helpers
{
    public class AccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public AccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor) { }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);
            if (user?.Identity == null) return null;
            if (!user.Identity.IsAuthenticated) return user;

            var identity = (ClaimsIdentity)user.Identity;
            
            var roleClaims = identity.FindAll(identity.RoleClaimType);

            if (!roleClaims.Any()) return user;

            var rolesElem = account.AdditionalProperties[identity.RoleClaimType];

            if (rolesElem is not JsonElement roles) return user;
            if (roles.ValueKind != JsonValueKind.Array) return user;

            identity.RemoveClaim(identity.FindFirst(options.RoleClaim));
            foreach (var role in roles.EnumerateArray())
            {
                identity.AddClaim(new Claim(options.RoleClaim, role.GetString() ?? string.Empty));
            }

            return user;
        }
    }
}
