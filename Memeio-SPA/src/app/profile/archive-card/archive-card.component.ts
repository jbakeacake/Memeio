import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-archive-card',
  templateUrl: './archive-card.component.html',
  styleUrls: ['./archive-card.component.css'],
})
export class ArchiveCardComponent implements OnInit {
  @Input() photo: Photo;
  @Input() isEditing: boolean;
  @Input() isSelected: boolean;
  @Output() selectionInfo = new EventEmitter();

  constructor() {}

  ngOnInit() {}

  enlargePhoto() {}

  emitSelectionInfo() {
    if (this.isEditing) {
      this.isSelected = !this.isSelected;
      this.selectionInfo.emit({
        id: this.photo.id,
        isSelected: this.isSelected,
      });
    }
  }
}
