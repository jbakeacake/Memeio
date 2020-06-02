import { Comments } from './comments';

export interface Photo {
    id: number;
    url: string;
    dateCreated: string;
    author: string;
    authorId: number;
    likes: number;
    dislikes: number;
    favorites: number;
    comments: Comments[];
}