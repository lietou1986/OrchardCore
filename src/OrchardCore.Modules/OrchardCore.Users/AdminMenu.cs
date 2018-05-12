using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Navigation;
using System;
using OrchardCore.Users.Drivers;
using OrchardCore.Modules;

namespace OrchardCore.Users
{
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public void BuildNavigation(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Security"], "5", security => security
                        .Add(T["Users"], "5", installed => installed
                            .Action("Index", "Admin", "OrchardCore.Users")
                            .Permission(Permissions.ManageUsers)
                            .LocalNav()
                         ))
                    .Add(T["Settings"], settings => settings
                        .Add(T["Users"], T["Users"], users => users
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = RegistrationSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));
        }
    }
    
    [Feature("OrchardCore.Users.Password")]
    public class PasswordAdminMenu : INavigationProvider
    {
        public PasswordAdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public void BuildNavigation(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Settings"], settings => settings
                        .Add(T["Password"], T["Password"], password => password
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = PasswordSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));
        }
    }
}
