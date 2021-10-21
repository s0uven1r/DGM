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
    {
      path: "promo",
      data: { breadcrumb: "Promo" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/package-course/promo/promo.module"
            ).then((m) => m.PromoModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/promo/promo-create/promo-create.module"
            ).then((m) => m.PromoCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/promo/promo-update/promo-update.module"
            ).then((m) => m.PromoUpdateModule),
        },
      ],
    },
    {
      path: "course",
      data: { breadcrumb: "Course" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course/course.module"
            ).then((m) => m.CourseModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course/course-create/course-create.module"
            ).then((m) => m.CourseCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course/course-update/course-update.module"
            ).then((m) => m.CourseUpdateModule),
        },
      ],
    },
    {
      path: "coursetype",
      data: { breadcrumb: "Course Type" },
      children: [
        {
          path: "",
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course-type/course-type.module"
            ).then((m) => m.CourseTypeModule),
        },
        {
          path: "create",
          data: { breadcrumb: "Create" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course-type/course-type-create/course-type-create.module"
            ).then((m) => m.CourseTypeCreateModule),
        },
        {
          path: "edit/:id",
          data: { breadcrumb: "Edit" },
          loadChildren: () =>
            import(
              "src/app/featured/package-course/course-type/course-type-update/course-type-update.module"
            ).then((m) => m.CourseTypeUpdateModule),
        },
      ],
    },
  ],
};
