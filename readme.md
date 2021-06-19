# Auth0 Blazor Wasm with ASP.NET Core API

Basic Blazor Wasm app with ASP.NET Core API backend that uses Auth0 for authentication and authorization.

## Installation

```c#
dotnet build
```

## Required Dependencies

### Frontend

[Microsoft.AspNetCore.Components.WebAssembly.Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Components.WebAssembly.Authentication/)

### Backend

[Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)

## Auth0

After following the setup linked below on References, do the following:

* Roles

  * Add new rule under **Auth Pipeline** on Auth0 dashboard

  * Name it what you want and add the following:

  * ```javascript
    function (user, context, callback) {
      context.idToken['https://schemas.quickstarts.com/roles'] = user.app_metadata.authorization.roles;
      context.idToken['https://schemas.quickstarts.com/groups'] = user.app_metadata.authorization.groups;
      context.idToken['https://schemas.quickstarts.com/permissions'] = user.app_metadata.authorization.permissions;
      return callback(null, user, context);
    }
    ```

  * Under **Roles** on Auth0 dashboard, create two new roles (**admin.user** and **normal.user**)

* Users

  * Add new user either thru app or under **User Management** on Auth dashboard

  * Name it what you want and add the following under **app_metadata**

  * ```json
    {
      "authorization": {
        "groups": [
          "admin.group",
          "normal.group"
        ],
        "roles": [
          "admin.user",
          "normal.user"
        ],
        "permissions": [
          "read.user",
          "write.user"
        ]
      }
    }
    ```

  * Don't forget to hit **Save**

## License

[MIT](https://choosealicense.com/licenses/mit/)

## References

[Auth0 Official Website](https://auth0.com/)

[Auth0 Blazor Setup](https://github.com/auth0-blog/secure-blazor-wasm-quiz-manager)

[Claims Principal Factory](https://medium.com/@marcodesanctis2/role-based-security-with-blazor-and-identity-server-4-aba12da70049)

