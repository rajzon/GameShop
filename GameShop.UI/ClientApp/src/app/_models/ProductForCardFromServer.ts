import { SubCategory } from './SubCategory';
import { Languague } from './Languague';
import { Stock } from './Stock';
import { Category } from './Category';
import { Photo } from './Photo';
import { Requirements } from './Requirements';
export interface ProductForCardFromServer {
    id: number;
    name: string;
    price: number;
    description: string;
    isDigitalMedia: boolean;
    pegi: number;
    releaseDate: Date;

    category: Category;
    photos: Photo[];
    stock: Stock;
    requirements: Requirements;
    languages: Languague[];
    subCategories: SubCategory[];
}
