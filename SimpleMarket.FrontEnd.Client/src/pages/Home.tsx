import { useState, useEffect } from 'react';
import { Container, Typography, Grid, Card, CardContent, CardMedia, Button, Box, Skeleton, IconButton } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';
import { getProductImage } from '../services/unsplashService';
import { ArrowBackIos, ArrowForwardIos } from '@mui/icons-material';
import Footer from '../components/Footer';

const featuredProducts = [
  {
    id: 1,
    name: 'Premium Headphones',
    price: 199.99,
    image: '',
    category: 'Electronics'
  },
  {
    id: 2,
    name: 'Smart Watch',
    price: 299.99,
    image: '',
    category: 'Electronics'
  },
  {
    id: 3,
    name: 'Wireless Earbuds',
    price: 149.99,
    image: '',
    category: 'Electronics'
  }
];

const slides = [
  {
    id: 1,
    title: "Summer Collection",
    description: "Discover the latest trends in summer fashion",
    image: "https://images.unsplash.com/photo-1445205170230-053b83016050?w=1600&auto=format&fit=crop&q=60",
    link: "/catalog?category=clothing"
  },
  {
    id: 2,
    title: "Tech Essentials",
    description: "Upgrade your digital lifestyle with our tech collection",
    image: "https://images.unsplash.com/photo-1550009158-9ebf69173e03?w=1600&auto=format&fit=crop&q=60",
    link: "/catalog?category=electronics"
  },
  {
    id: 3,
    title: "Home & Living",
    description: "Transform your space with our home essentials",
    image: "https://images.unsplash.com/photo-1484101403633-562f891dc89a?w=1600&auto=format&fit=crop&q=60",
    link: "/catalog?category=home"
  }
];

const Home = () => {
  const [productsWithImages, setProductsWithImages] = useState(featuredProducts);
  const [loading, setLoading] = useState(true);
  const [currentSlide, setCurrentSlide] = useState(0);

  useEffect(() => {
    const loadImages = async () => {
      const updatedProducts = await Promise.all(
        featuredProducts.map(async (product) => ({
          ...product,
          image: await getProductImage(product.name, product.category)
        }))
      );
      setProductsWithImages(updatedProducts);
      setLoading(false);
    };

    loadImages();
  }, []);

  useEffect(() => {
    const timer = setInterval(() => {
      setCurrentSlide((prev) => (prev + 1) % slides.length);
    }, 5000);

    return () => clearInterval(timer);
  }, []);

  const handlePrevSlide = () => {
    setCurrentSlide((prev) => (prev - 1 + slides.length) % slides.length);
  };

  const handleNextSlide = () => {
    setCurrentSlide((prev) => (prev + 1) % slides.length);
  };

  return (
    <Box 
      sx={{ 
        display: 'flex', 
        flexDirection: 'column', 
        minHeight: '100vh',
        bgcolor: 'background.default',
        position: 'relative',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          height: '100vh',
          background: `
            radial-gradient(circle at top right, rgba(25, 118, 210, 0.2) 0%, transparent 50%),
            radial-gradient(circle at bottom left, rgba(33, 150, 243, 0.15) 0%, transparent 50%),
            linear-gradient(45deg, rgba(25, 118, 210, 0.1) 0%, rgba(33, 150, 243, 0.1) 100%)
          `,
          pointerEvents: 'none',
          zIndex: 0
        }
      }}
    >
      {/* Hero Section with Slider */}
      <Box
        sx={{
          position: 'relative',
          height: { xs: '60vh', md: '80vh' },
          overflow: 'hidden',
          bgcolor: 'primary.main',
          '&::before': {
            content: '""',
            position: 'absolute',
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background: `
              linear-gradient(45deg, rgba(0,0,0,0.5) 0%, rgba(0,0,0,0.3) 100%),
              url("data:image/svg+xml,%3Csvg width='60' height='60' viewBox='0 0 60 60' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='none' fill-rule='evenodd'%3E%3Cg fill='%232196f3' fill-opacity='0.1'%3E%3Cpath d='M36 34v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6 34v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6 4V0H4v4H0v2h4v4h2V6h4V4H6z'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E")
            `,
            zIndex: 1
          }
        }}
      >
        {slides.map((slide, index) => (
          <Box
            key={slide.id}
            sx={{
              position: 'absolute',
              top: 0,
              left: 0,
              right: 0,
              bottom: 0,
              opacity: currentSlide === index ? 1 : 0,
              transition: 'all 0.8s ease-in-out',
              backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)), url(${slide.image})`,
              backgroundSize: 'cover',
              backgroundPosition: 'center',
              transform: `scale(${currentSlide === index ? 1 : 1.1})`,
            }}
          >
            <Container
              sx={{
                height: '100%',
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                textAlign: 'center',
                color: 'white',
                position: 'relative',
                zIndex: 2,
                opacity: currentSlide === index ? 1 : 0,
                transform: `translateY(${currentSlide === index ? 0 : 20}px)`,
                transition: 'all 0.8s ease-in-out',
              }}
            >
              <Typography
                variant="h1"
                component="h1"
                sx={{
                  fontSize: { xs: '2.5rem', sm: '3.5rem', md: '4.5rem' },
                  fontWeight: 800,
                  letterSpacing: '-0.02em',
                  mb: 2,
                  textShadow: '2px 2px 4px rgba(0,0,0,0.3)',
                  background: 'linear-gradient(45deg, #fff 30%, #f0f0f0 90%)',
                  WebkitBackgroundClip: 'text',
                  WebkitTextFillColor: 'transparent',
                }}
              >
                {slide.title}
              </Typography>
              <Typography
                variant="h5"
                component="h2"
                sx={{
                  fontSize: { xs: '1.25rem', md: '1.5rem' },
                  maxWidth: '800px',
                  mb: 4,
                  opacity: 0.9,
                  lineHeight: 1.6,
                  textShadow: '1px 1px 2px rgba(0,0,0,0.3)',
                }}
              >
                {slide.description}
              </Typography>
              <Button
                variant="contained"
                color="secondary"
                size="large"
                component={RouterLink}
                to={slide.link}
                sx={{
                  px: 4,
                  py: 1.5,
                  fontSize: '1.1rem',
                  fontWeight: 600,
                  borderRadius: '12px',
                  boxShadow: '0 4px 14px rgba(0,0,0,0.1)',
                  '&:hover': {
                    transform: 'translateY(-2px)',
                    boxShadow: '0 6px 20px rgba(0,0,0,0.15)',
                    transition: 'all 0.3s ease',
                  },
                }}
              >
                Shop Now
              </Button>
            </Container>
          </Box>
        ))}

        {/* Navigation Buttons */}
        <IconButton
          onClick={handlePrevSlide}
          sx={{
            position: 'absolute',
            left: 16,
            top: '50%',
            transform: 'translateY(-50%)',
            color: 'white',
            bgcolor: 'rgba(0,0,0,0.3)',
            backdropFilter: 'blur(4px)',
            '&:hover': {
              bgcolor: 'rgba(0,0,0,0.5)',
              transform: 'translateY(-50%) scale(1.1)',
            },
            zIndex: 3,
          }}
        >
          <ArrowBackIos />
        </IconButton>
        <IconButton
          onClick={handleNextSlide}
          sx={{
            position: 'absolute',
            right: 16,
            top: '50%',
            transform: 'translateY(-50%)',
            color: 'white',
            bgcolor: 'rgba(0,0,0,0.3)',
            backdropFilter: 'blur(4px)',
            '&:hover': {
              bgcolor: 'rgba(0,0,0,0.5)',
              transform: 'translateY(-50%) scale(1.1)',
            },
            zIndex: 3,
          }}
        >
          <ArrowForwardIos />
        </IconButton>

        {/* Slide Indicators */}
        <Box
          sx={{
            position: 'absolute',
            bottom: 24,
            left: '50%',
            transform: 'translateX(-50%)',
            display: 'flex',
            gap: 1,
            zIndex: 3,
          }}
        >
          {slides.map((_, index) => (
            <Box
              key={index}
              onClick={() => setCurrentSlide(index)}
              sx={{
                width: 12,
                height: 12,
                borderRadius: '50%',
                bgcolor: currentSlide === index ? 'white' : 'rgba(255,255,255,0.5)',
                cursor: 'pointer',
                transition: 'all 0.3s ease',
                '&:hover': {
                  bgcolor: 'white',
                  transform: 'scale(1.2)',
                },
              }}
            />
          ))}
        </Box>
      </Box>

      {/* Featured Products */}
      <Container 
        maxWidth="xl" 
        sx={{ 
          py: { xs: 4, md: 6 }, 
          flex: 1,
          position: 'relative',
          '&::before': {
            content: '""',
            position: 'absolute',
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background: `
              radial-gradient(circle at 50% 50%, rgba(25, 118, 210, 0.1) 0%, transparent 50%),
              url("data:image/svg+xml,%3Csvg width='20' height='20' viewBox='0 0 20 20' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='%231976d2' fill-opacity='0.05' fill-rule='evenodd'%3E%3Ccircle cx='3' cy='3' r='3'/%3E%3Ccircle cx='13' cy='13' r='3'/%3E%3C/g%3E%3C/svg%3E")
            `,
            pointerEvents: 'none'
          }
        }}
      >
        <Typography 
          variant="h4" 
          component="h2" 
          gutterBottom
          sx={{ 
            textAlign: 'center',
            mb: 4,
            color: 'text.primary',
            position: 'relative',
            '&::after': {
              content: '""',
              position: 'absolute',
              bottom: -8,
              left: '50%',
              transform: 'translateX(-50%)',
              width: 60,
              height: 3,
              background: 'linear-gradient(90deg, transparent, #1976d2, transparent)',
              borderRadius: '2px',
            }
          }}
        >
          Featured Products
        </Typography>
        <Grid container spacing={3}>
          {productsWithImages.map((product) => (
            <Grid item xs={12} sm={6} md={4} key={product.id}>
              <Card 
                sx={{ 
                  height: '100%', 
                  display: 'flex', 
                  flexDirection: 'column',
                  bgcolor: 'background.paper',
                  overflow: 'hidden',
                  position: 'relative',
                  '&::before': {
                    content: '""',
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    right: 0,
                    height: '4px',
                    background: 'linear-gradient(90deg, #1976d2, #2196f3)',
                    opacity: 0,
                    transition: 'opacity 0.3s ease',
                  },
                  '&:hover': {
                    '&::before': {
                      opacity: 1,
                    },
                    '& .MuiCardMedia-root': {
                      transform: 'scale(1.05)',
                    },
                    '& .product-price': {
                      color: 'primary.main',
                    }
                  }
                }}
              >
                {loading ? (
                  <Skeleton variant="rectangular" height={200} />
                ) : (
                  <CardMedia
                    component="img"
                    height="200"
                    image={product.image}
                    alt={product.name}
                    sx={{ 
                      objectFit: 'cover',
                      transition: 'transform 0.5s ease-in-out'
                    }}
                  />
                )}
                <CardContent sx={{ flexGrow: 1 }}>
                  <Typography 
                    gutterBottom 
                    variant="h5" 
                    component="h2"
                    sx={{ 
                      color: 'text.primary',
                      fontWeight: 600,
                      mb: 1
                    }}
                  >
                    {product.name}
                  </Typography>
                  <Typography 
                    variant="body2" 
                    color="text.secondary"
                    sx={{ 
                      mb: 1,
                      display: 'flex',
                      alignItems: 'center',
                      gap: 0.5
                    }}
                  >
                    {product.category}
                  </Typography>
                  <Typography 
                    variant="h6" 
                    className="product-price"
                    sx={{ 
                      mt: 1,
                      fontWeight: 600,
                      transition: 'color 0.3s ease'
                    }}
                  >
                    ${product.price}
                  </Typography>
                  <Button
                    variant="contained"
                    color="primary"
                    fullWidth
                    sx={{ 
                      mt: 2,
                      bgcolor: 'primary.main',
                      '&:hover': {
                        bgcolor: 'primary.dark',
                        transform: 'translateY(-2px)',
                      },
                      transition: 'all 0.3s ease'
                    }}
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

export default Home; 
