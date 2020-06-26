import { Photo } from 'src/app/_models/Photo';
export interface ProductForSearching {
    id: number;
    name: string;
    price: Number;
    categoryName: string;
    photo: Photo;

}
