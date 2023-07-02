export interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
  productCategories: ProductCategory[];
  photos: Photo[];
}

export interface Photo {
  id: string;
  photoUrl: string;
}

export interface ProductCategory {
  id: string;
  name: string;
  description: string;
}
