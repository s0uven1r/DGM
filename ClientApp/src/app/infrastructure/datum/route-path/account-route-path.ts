export const AccountRoutePath = {
  path: "account",
  data: { breadcrumb: "Account" },
  children: [
    {
      path: "accounttype",
      data: { breadcrumb: "Account Type" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/account/account-type/account-type.module"
            ).then((m) => m.AccountTypeModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/account/account-type/account-type-create/account-type-create.module"
            ).then((m) => m.AccountTypeCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/account/account-type/account-type-edit/account-type-edit.module"
            ).then((m) => m.AccountTypeEditModule),
        },
      ],
    },
    {
      path: "accounthead",
      data: { breadcrumb: "Account Head" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/account/account-head/account-head.module"
            ).then((m) => m.AccountHeadModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/account/account-head/account-head-create/account-head-create.module"
            ).then((m) => m.AccountHeadCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/account/account-head/account-head-edit/account-head-edit.module"
            ).then((m) => m.AccountHeadEditModule),
        },
      ],
    },
  ],
};
