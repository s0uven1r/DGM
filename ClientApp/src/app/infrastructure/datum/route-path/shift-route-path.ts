export const ShiftRoutePath = {
    path: "shift-config",
    data: { breadcrumb: "Shift Configuration" },
    children: [
      {
        path:"frequency",
        data:{breadcrumb: "frequency"},
        children:[
          {
            path: "",
            loadChildren: () =>
              import(
                "src/app/featured/shift/frequency/shift-frequency.module"
              ).then((m) => m.ShiftFrequencyModule),
          },
          {
            path: "create",
            data: { breadcrumb: "Create" },
            loadChildren: () =>
              import(
                "src/app/featured/shift/frequency/frequency-create/shift-frequency-create.module"
              ).then((m) => m.ShiftFrequencyCreateModule),
          }
        ]
      },
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
            data: { breadcrumb: "Create" },
            loadChildren: () =>
              import(
                "src/app/featured/shift/shift/shift-create/shift-create.module"
              ).then((m) => m.ShiftCreateModule),
          },
          {
            path: "update",
            data: { breadcrumb: "Update" },
            loadChildren: () =>
              import(
                "src/app/featured/shift/shift/shift-update/shift-update.module"
              ).then((m) => m.ShiftUpdateModule),
          }
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
      }
    ]
  };
  