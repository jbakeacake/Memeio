import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css'],
})
export class PostCardComponent implements OnInit {
  @Input() photo: Photo;
  @Input() isEditing: boolean;
  @Input() isSelected: boolean;
  @Output() selectionInfo = new EventEmitter();

  constructor() {}

  ngOnInit() {
  }

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
