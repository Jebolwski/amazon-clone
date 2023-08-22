export interface Bought {
  timeBought: Date;
  user: User;
  products: Product[];
}

export interface Product {
  id: string;
  name: string;
  price: string;
  description: string;
  comments: Comment[];
  productCategories: any[];
  photos: Photo[];
  count?: number;
}

export interface Comment {
  id: string;
  user: null;
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
  id: string;
  roleId: string;
  username: string;
  cartId: string;
  tokenCreated: Date;
  tokenExpires: Date;
}
