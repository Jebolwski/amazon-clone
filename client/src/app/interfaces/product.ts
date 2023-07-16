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
  id: string;
  user: User;
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

export interface User {
  roleId: string;
  username: string;
  passwordHash: string;
  passwordSalt: string;
  refreshToken: string;
  cartId: string;
  tokenCreated: Date;
  tokenExpires: Date;
  id: string;
}

export interface ProductCategory {
  id: string;
  name: string;
  description: string;
}
