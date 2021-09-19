export const ConfigRoutePath = {
  path: "config",
  data: { breadcrumb: "Config" },
  children: [
    {
      path: "package",
      data: { breadcrumb: "Package" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/package-course/package/package.module"
            ).then((m) => m.PackageModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/package/package-create/package-create.module"
            ).then((m) => m.PackageCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/package/package-update/package-update.module"
            ).then((m) => m.PackageUpdateModule),
        },
      ],
    },
  ],
};
