import { Component } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { MENU_ENTRIES_FULL, MENU_ENTRIES_MOBILE } from '../menu-entries';

const DEFAULT_AVATAR_URL = './assets/outline_face_black_48dp.png'

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.scss']
})
export class FrameComponent {
  protected readonly desktopEntries = MENU_ENTRIES_FULL;
  protected readonly mobileEntries = MENU_ENTRIES_MOBILE;

  //todo Update to using CurrentUserStream when implemented
  currentUserName$ = of('User');
  userAvatarUrl$ = new BehaviorSubject<string>('');

  useDefaultImage() {
    this.userAvatarUrl$.next(DEFAULT_AVATAR_URL);
  }
}
