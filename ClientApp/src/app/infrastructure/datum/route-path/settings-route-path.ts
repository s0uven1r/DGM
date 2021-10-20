export const SettingsRoutePath = {
    path: "settings",
    data: { breadcrumb: "Settings" },
    children: [
      {
        path: "logo",
        data: { breadcrumb: "Logo" },
        children: [
          {
            path: "",
            loadChildren: () =>
              import(
                "src/app/featured/settings/logo/logo.module"
              ).then((m) => m.LogoModule),
          }
        ],
      }
    ],
  };