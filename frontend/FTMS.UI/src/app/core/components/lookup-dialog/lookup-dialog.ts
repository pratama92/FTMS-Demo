import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { LookupItem } from '../../models/lookup-item.model';

@Component({
  selector: 'app-lookup-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './lookup-dialog.html',
  styleUrl: './lookup-dialog.scss'
})
export class LookupDialog {

  keyword = '';

  @Input()
  title = 'Lookup';

  @Input()
  visible = false;

  @Input()
  items: LookupItem[] = [];

  @Output()
  close = new EventEmitter<void>();

  @Output()
  selected = new EventEmitter<LookupItem>();

  get filteredItems(): LookupItem[] {

    if (!this.keyword.trim()) {
      return this.items;
    }

    return this.items.filter(item =>
      item.lookupName
        .toLowerCase()
        .includes(this.keyword.toLowerCase())
    );

  }

}