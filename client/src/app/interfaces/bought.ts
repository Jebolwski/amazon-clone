export interface Bought {
  id: string;
  timeBought: string | Date;
  user: User;
  products: Product[];
}

export interface Product {
  boughtId: string;
  id: string;
  name: string;
  price: string;
  description: string;
  comments: Comment[];
  productCategories: any[];
  photos: Photo[];
  productId: string;
  count: number;
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
