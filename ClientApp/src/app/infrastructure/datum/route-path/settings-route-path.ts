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
      },
      {
        path: "descriptiveimage",
        data: { breadcrumb: "DescriptiveImage" },
        children: [
          {
            path: "",
            loadChildren: () =>
              import(
                "src/app/featured/settings/descriptive-image/descriptive-image.module"
              ).then((m) => m.DescriptiveImageModule),
          }
        ],
      }
    ],
  };