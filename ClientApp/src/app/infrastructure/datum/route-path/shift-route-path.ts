export const ShiftRoutePath = {
    path: "shift-config",
    data: { breadcrumb: "Shift Configuration" },
    children: [
      {
        path:"shift",
        data:{breadcrumb: "shift"},
        children:[
          {
            path: "",
            loadChildren: () =>
              import(
                "src/app/featured/shift/shift/shift.module"
              ).then((m) => m.ShiftModule),
          },
          {
            path: "create",
            data: { breadcrumb: "Edit" },
            loadChildren: () =>
              import(
                "src/app/featured/shift/shift/shift-create/shift-create.module"
              ).then((m) => m.ShiftCreateModule),
          },
        ]
      },
      
      {
        path: "individual-shift",
        data: { breadcrumb: "Individual Shift" },
        children: [
          {
            path: "",
            loadChildren: () =>
              import(
                "src/app/featured/shift/individual-shift/individual-shift.module"
              ).then((m) => m.IndividualShiftModule),
          },
          {
            path: "edit/:id",
            data: { breadcrumb: "Edit" },
            loadChildren: () =>
              import(
                "src/app/featured/shift/individual-shift/edit-individual-shift/edit-individual-shift.module"
              ).then((m) => m.EditIndividualShiftModule),
          },
        ]
      },
      
    ]
  };
  