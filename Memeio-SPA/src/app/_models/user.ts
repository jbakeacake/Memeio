import { Photo } from './photo';
import { Comments } from './comments';

export interface User {
    id: number;
    username: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    introduction?: string;
    posts?: Photo[];
    comments?: Comments[];
}