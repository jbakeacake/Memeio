import { Photo } from './photo';
import { Comments } from './comments';

export interface User {
    id: number;
    username: string;
    dateCreated: Date;
    lastActive: Date;
    photoUrl: string;
    likes?: number;
    dislikes?: number;
    introduction?: string;
    posts?: Photo[];
    archived?: Photo[];
    comments?: Comments[];
    followers?: number;
    follows?: number;
}