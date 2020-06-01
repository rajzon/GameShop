export interface ProductFromServer {
    id: number;
    name: string;
    price: number;
    categoryName: string;
    subCategories: string[];
    releaseDate: Date;

    pegi?: number;
    description?: string;
    languages?: string[];
    photos?: string[];
    isDigitalMedia?: boolean;
}
