export const ShiftRoutePath = {
    path: "shift-config",
    data: { breadcrumb: "Shift Configuration" },
    children: [
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
        ],
      },
    ],
  };
  