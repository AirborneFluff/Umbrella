import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SideMenuLayouts } from "../side-menu-layouts";
import { RootFeatureStream } from '../../streams/root-feature-stream';
import { RootFeatures } from '../../definitions/root-features';
import { notNullOrUndefined } from '../../pipes/not-null';
import { AccountService } from '../../services/account.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent {
  @Input() layoutType!: string;
  @Input() showCloseButton: boolean = false;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  showDefaultAvatar = false;

  constructor(private root$: RootFeatureStream, protected account: AccountService) {}

  protected activeRoot$ = this.root$.pipe(notNullOrUndefined())

  accountInitial$ = this.account.currentUser$.pipe(
    notNullOrUndefined(),
    map(user => user.email[0].toUpperCase())
  )

  get widthClass(): string {
    if (this.layoutType == SideMenuLayouts.Full) return 'w-full';
    if (this.layoutType == SideMenuLayouts.Regular) return 'w-72';
    return 'w-24';
  }

  get isCompact(): boolean {
    return this.layoutType == SideMenuLayouts.Compact;
  }

  protected readonly SideMenuLayouts = SideMenuLayouts;
  protected readonly RootFeatures = RootFeatures;
}
