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
        ],
      },
    ],
  };
  