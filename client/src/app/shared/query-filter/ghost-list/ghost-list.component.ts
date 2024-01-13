import { Component } from '@angular/core';

@Component({
  selector: 'filter-ghost-list',
  templateUrl: './ghost-list.component.html',
  styleUrls: ['./ghost-list.component.scss']
})
export class GhostListComponent {
  ghosts: any[] = Array.from({ length: 5 });

}
