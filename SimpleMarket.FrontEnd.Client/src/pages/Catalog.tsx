import { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  Grid,
  Card,
  CardContent,
  CardMedia,
  Button,
  Box,
  Tabs,
  Tab,
  TextField,
  InputAdornment,
  Skeleton
} from '@mui/material';
import { Search } from '@mui/icons-material';
import type { GridProps } from '@mui/material';
import { getProductImage } from '../services/unsplashService';
import Footer from '../components/Footer';

const categories = [
  'All',
  'Electronics',
  'Clothing',
  'Home & Garden',
  'Sports',
  'Books'
];

const products = [
  {
    id: 1,
    name: 'Premium Headphones',
    price: 199.99,
    image: '',
    category: 'Electronics',
    description: 'High-quality wireless headphones with noise cancellation'
  },
  {
    id: 2,
    name: 'Smart Watch',
    price: 299.99,
    image: '',
    category: 'Electronics',
    description: 'Feature-rich smartwatch with health monitoring'
  },
  {
    id: 3,
    name: 'Wireless Earbuds',
    price: 149.99,
    image: '',
    category: 'Electronics',
    description: 'True wireless earbuds with premium sound quality'
  },
  {
    id: 4,
    name: 'Running Shoes',
    price: 89.99,
    image: '',
    category: 'Sports',
    description: 'Comfortable running shoes for all terrains'
  },
  {
    id: 5,
    name: 'Yoga Mat',
    price: 29.99,
    image: '',
    category: 'Sports',
    description: 'Non-slip yoga mat with carrying strap'
  },
  {
    id: 6,
    name: 'Coffee Maker',
    price: 79.99,
    image: '',
    category: 'Home & Garden',
    description: 'Programmable coffee maker with thermal carafe'
  },
  {
    id: 7,
    name: 'Novel Collection',
    price: 49.99,
    image: '',
    category: 'Books',
    description: 'Set of 5 bestselling novels'
  },
  {
    id: 8,
    name: 'Designer T-Shirt',
    price: 39.99,
    image: '',
    category: 'Clothing',
    description: 'Premium cotton t-shirt with unique design'
  }
];

const Catalog = () => {
  const [selectedCategory, setSelectedCategory] = useState('All');
  const [searchQuery, setSearchQuery] = useState('');
  const [productsWithImages, setProductsWithImages] = useState(products);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadImages = async () => {
      const updatedProducts = await Promise.all(
        products.map(async (product) => ({
          ...product,
          image: await getProductImage(product.name, product.category)
        }))
      );
      setProductsWithImages(updatedProducts);
      setLoading(false);
    };

    loadImages();
  }, []);

  const filteredProducts = productsWithImages.filter(product => {
    const matchesCategory = selectedCategory === 'All' || product.category === selectedCategory;
    const matchesSearch = product.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
                         product.description.toLowerCase().includes(searchQuery.toLowerCase());
    return matchesCategory && matchesSearch;
  });

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <Container maxWidth="xl" sx={{ py: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Product Catalog
        </Typography>

        {/* Search and Filter Section */}
        <Box sx={{ mb: 4 }}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={6}>
              <TextField
                fullWidth
                placeholder="Search products..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <Search />
                    </InputAdornment>
                  ),
                }}
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <Box sx={{ 
                width: '100%', 
                overflowX: 'auto',
                '& .MuiTabs-root': {
                  minWidth: { xs: '100%', sm: 'auto' }
                }
              }}>
                <Tabs
                  value={selectedCategory}
                  onChange={(_, newValue) => setSelectedCategory(newValue)}
                  variant="scrollable"
                  scrollButtons="auto"
                  allowScrollButtonsMobile
                >
                  {categories.map((category) => (
                    <Tab key={category} label={category} value={category} />
                  ))}
                </Tabs>
              </Box>
            </Grid>
          </Grid>
        </Box>

        {/* Products Grid */}
        <Grid container spacing={3}>
          {filteredProducts.map((product) => (
            <Grid item key={product.id} xs={12} sm={6} md={4} lg={3}>
              <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
                {loading ? (
                  <Skeleton variant="rectangular" height={200} />
                ) : (
                  <CardMedia
                    component="img"
                    height="200"
                    image={product.image}
                    alt={product.name}
                    sx={{ objectFit: 'cover' }}
                  />
                )}
                <CardContent sx={{ flexGrow: 1 }}>
                  <Typography gutterBottom variant="h6" component="h2">
                    {product.name}
                  </Typography>
                  <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                    {product.category}
                  </Typography>
                  <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                    {product.description}
                  </Typography>
                  <Typography variant="h6" color="primary" sx={{ mt: 1 }}>
                    ${product.price}
                  </Typography>
                  <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    sx={{ mt: 2 }}
                  >
                    Add to Cart
                  </Button>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>

      {/* Footer */}
      <Footer />
    </Box>
  );
};

export default Catalog; 
