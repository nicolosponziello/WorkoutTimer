using System.Collections.Generic;

namespace WorkoutTimer
{
    public class NotificationPermission : Xamarin.Essentials.Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new List<(string androidPermission, bool isRuntime)>
        {
            ("android.permission.POST_NOTIFICATIONS", true),
        }.ToArray();
    }
}