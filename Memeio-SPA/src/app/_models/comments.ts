export interface Comments {
    id: number;
    author: string;
    authorId: string; // Id of the user making the comment
    content: string;
    userId: string; // The profile Id
}