import { Component } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';

const DEFAULT_AVATAR_URL = './assets/outline_face_black_48dp.png'

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.scss']
})
export class FrameComponent {
  //todo Update to using CurrentUserStream when implemented
  currentUserName$ = of('User');
  userAvatarUrl$ = new BehaviorSubject<string>('');

  useDefaultImage() {
    this.userAvatarUrl$.next(DEFAULT_AVATAR_URL);
  }
}
