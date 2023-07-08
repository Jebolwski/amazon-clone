export interface UpdateProduct {
  id: string;
  name: string;
  price: number;
  description: string;
  photos: Photo[];
  productCategories: ProductCategory[];
}

export interface Photo {
  photoUrl: string;
}

export interface ProductCategory {
  id: string;
}
