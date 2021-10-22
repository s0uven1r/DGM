import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  HostBinding,
  ChangeDetectorRef,
} from "@angular/core";
import { ChildActivationEnd, Router } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { Subscription } from "rxjs";
import { filter } from "rxjs/operators";
import { LogoService } from "src/app/featured/settings/service/logo.service";
import { MenuResultModel } from "src/app/infrastructure/model/UserManagement/Menu/menu-result-model";
import { NavigationService } from "./services";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.css"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent implements OnInit {
  user: any;
  subscription: Subscription = new Subscription();
  sidenavStyle = "sb-sidenav-dark";
  expandNavStyle = "";
  expandContentStyle = "";
  expanded = false;
  menuList: MenuResultModel[];
  imageToShow: any;
  isImageLoading = false;
  constructor(
    public router: Router,
    private authService: OAuthService,
    public navigationService: NavigationService,
    public logoService: LogoService,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.user = this.authService.getIdentityClaims();
    this.router.events
      .pipe(filter((event) => event instanceof ChildActivationEnd))
      .subscribe((event) => {
        let snapshot = (event as ChildActivationEnd).snapshot;
        while (snapshot.firstChild !== null) {
          snapshot = snapshot.firstChild;
        }
      });
  }

  ngOnInit(): void {
    this.getImageFromService();
    this.subscription.add(
      this.navigationService.getMenu().subscribe((x) => {
        this.menuList = x;
        this.changeDetectorRef.markForCheck();
      })
    );
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  toggleSideNav() {
    if (this.expanded == false) {
      this.expanded = true;
      this.expandNavStyle = "collapse";
      this.expandContentStyle = "layoutMarginLeft";
    } else {
      this.expanded = false;
      this.expandNavStyle = "";
      this.expandContentStyle = "";
    }
  }
  getLogout() {
    this.authService.revokeTokenAndLogout();
  }

  getImageFromService() {
    this.isImageLoading = true;
    this.subscription.add(
      this.logoService.getLogo().subscribe(
        (data) => {
          this.createImageFromBlob(data);
          this.isImageLoading = false;
        },
        (error) => {
          this.isImageLoading = false;
          console.log(error);
        }
      )
    );
  }
  createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener(
      "load",
      () => {
        this.imageToShow = reader.result;
      },
      false
    );

    if (image) {
      reader.readAsDataURL(image);
    }
  }
}
