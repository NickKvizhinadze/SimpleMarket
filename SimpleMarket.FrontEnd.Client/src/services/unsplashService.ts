const UNSPLASH_ACCESS_KEY = 'BDN8RCKVydW1vuaW5fegDper3bb6KUXQ4KI5p2o9Wqw'; // You'll need to replace this with your actual Unsplash API key
const UNSPLASH_API_URL = 'https://api.unsplash.com';

// Fallback images for different categories
const FALLBACK_IMAGES = {
  Electronics: 'https://images.unsplash.com/photo-1550009158-9ebf69173e03?w=800&auto=format&fit=crop&q=60',
  Clothing: 'https://images.unsplash.com/photo-1445205170230-053b83016050?w=800&auto=format&fit=crop&q=60',
  'Home & Garden': 'https://images.unsplash.com/photo-1484101403633-562f891dc89a?w=800&auto=format&fit=crop&q=60',
  Sports: 'https://images.unsplash.com/photo-1461896836934-ffe607ba8211?w=800&auto=format&fit=crop&q=60',
  Books: 'https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=800&auto=format&fit=crop&q=60',
  default: 'https://images.unsplash.com/photo-1498049794561-7780e7231661?w=800&auto=format&fit=crop&q=60'
};

export const getRandomImage = async (query: string): Promise<string> => {
  try {
    const response = await fetch(
      `${UNSPLASH_API_URL}/photos/random?query=${encodeURIComponent(query)}&orientation=landscape&client_id=${UNSPLASH_ACCESS_KEY}`
    );
    
    if (!response.ok) {
      throw new Error(`Unsplash API error: ${response.status}`);
    }
    
    const data = await response.json();
    return data.urls.regular;
  } catch (error) {
    console.error('Error fetching image from Unsplash:', error);
    return FALLBACK_IMAGES.default;
  }
};

export const getProductImage = async (productName: string, category: string): Promise<string> => {
  try {
    // First try to get a category-specific image
    const categoryImage = await getRandomImage(category);
    if (categoryImage !== FALLBACK_IMAGES.default) {
      return categoryImage;
    }
    
    // If category image fails, try with product name
    const productImage = await getRandomImage(productName);
    if (productImage !== FALLBACK_IMAGES.default) {
      return productImage;
    }
    
    // If both fail, return category-specific fallback
    return FALLBACK_IMAGES[category as keyof typeof FALLBACK_IMAGES] || FALLBACK_IMAGES.default;
  } catch (error) {
    console.error('Error in getProductImage:', error);
    return FALLBACK_IMAGES[category as keyof typeof FALLBACK_IMAGES] || FALLBACK_IMAGES.default;
  }
}; 
