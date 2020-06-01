import { Requirements } from './Requirements';
export interface Product {
    name: string;
    description: string;
    price: number;
    pegi: number;
    requirements: Requirements;
    categoryId: number;
    subCategoriesId: number[];
    languagesId: number[];
    photos: string[];
    isDigitalMedia: boolean;
    //releaseDate: Date;
}
