namespace CimmytApp.Core
{
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;

    public static class PermissionHelper
    {
        public static async Task<bool> HasPermissionAsync(Permission permission)
        {
            var permissionStatus = await CheckPermissionAsync(permission);
            return permissionStatus == PermissionStatus.Granted;
        }

        public static async Task<bool> CheckAndRequestPermissionAsync(Permission permission, string requestTitle,
            string requestText, string confirmText, string deniedTitle, string deniedText)
        {

            var permissionStatus = await CheckPermissionAsync(permission);

            if (permissionStatus != PermissionStatus.Granted)
            {
                if (!await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    await UserDialogs.Instance.AlertAsync(requestText, requestTitle, confirmText);
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                if (results.ContainsKey(Permission.Location))
                    permissionStatus = results[Permission.Location];
            }

            if (permissionStatus == PermissionStatus.Granted)
            {
                return true;
            }

            if (permissionStatus != PermissionStatus.Unknown)
            {
                UserDialogs.Instance.Alert(deniedText, deniedTitle, confirmText);
            }

            return false;
        }
        private static async Task<PermissionStatus> CheckPermissionAsync(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            return permissionStatus;
        }
    }
}