export interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
  comments: Comment[];
  productCategories: ProductCategory[];
  photos: Photo[];
}

export interface Comment {
  userId: string;
  comment: string;
  commentPhotos: Photo[];
  productId: string;
  title: string;
  stars: number;
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
