import { Component } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { MENU_ENTRIES_FULL, MENU_ENTRIES_MOBILE } from '../menu-entries';
import { MenuEntry } from '../menu-entry';
import { Router } from '@angular/router';

const DEFAULT_AVATAR_URL = './assets/outline_face_black_48dp.png'

@Component({
  selector: 'app-frame',
  templateUrl: './app-frame.component.html',
  styleUrls: ['./app-frame.component.scss']
})
export class AppFrameComponent {
  protected readonly desktopEntries = MENU_ENTRIES_FULL;
  protected readonly mobileEntries = MENU_ENTRIES_MOBILE;

  constructor(private router: Router) {
  }

  //todo Update to using CurrentUserStream when implemented
  currentUserName$ = of('User');
  userAvatarUrl$ = new BehaviorSubject<string>('');
  selectedEntryIndex: number = 0;

  useDefaultImage() {
    this.userAvatarUrl$.next(DEFAULT_AVATAR_URL);
  }

  //todo Not good, try some other solution
  isSelectedEntry(entry: MenuEntry, entries: MenuEntry[]) {
    const index = entries.indexOf(entry);
    return index == this.selectedEntryIndex;
  }

  handleEntryClick(entry: MenuEntry) {
    this.router.navigateByUrl(entry.link);
  }
}
