import { Box, Container, Grid, Typography, Link, IconButton, TextField, Button } from '@mui/material';
import { Facebook, Twitter, Instagram, LinkedIn, Send } from '@mui/icons-material';
import logo from '../assets/logo.png';

const Footer = () => {
  return (
    <Box
      component="footer"
      sx={{
        position: 'relative',
        background: 'linear-gradient(135deg, #1976d2 0%, #2196f3 100%)',
        color: '#ffffff',
        py: { xs: 6, md: 8 },
        mt: 'auto',
        boxShadow: '0 -4px 20px rgba(25, 118, 210, 0.15)',
        width: '100%',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          height: '4px',
          background: 'linear-gradient(90deg, #64b5f6 0%, #2196f3 50%, #64b5f6 100%)',
        },
        '&::after': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundImage: 'radial-gradient(circle at 1px 1px, rgba(255, 255, 255, 0.1) 1px, transparent 0)',
          backgroundSize: '20px 20px',
          opacity: 0.5,
          pointerEvents: 'none',
        }
      }}
    >
      <Container maxWidth="xl" sx={{ px: { xs: 2, sm: 3, md: 4 }, position: 'relative' }}>
        <Grid container spacing={4}>
          <Grid item xs={12} sm={4}>
            <Box
              component="img"
              src={logo}
              alt="SimpleMarket Logo"
              sx={{
                height: '40px',
                mb: 3,
                transition: 'all 0.3s ease-in-out',
                '&:hover': {
                  transform: 'scale(1.05)',
                  filter: 'drop-shadow(0 0 8px rgba(255, 255, 255, 0.5))',
                }
              }}
            />
            <Typography 
              variant="body2" 
              sx={{ 
                color: 'rgba(255, 255, 255, 0.9)',
                lineHeight: 1.7,
                fontSize: '0.95rem',
                maxWidth: '300px',
                mb: 3
              }}
            >
              Your one-stop shop for all your needs. Quality products, great prices, and excellent service.
            </Typography>
            <Box 
              component="form" 
              sx={{ 
                display: 'flex', 
                gap: 1,
                maxWidth: '300px',
                '& .MuiOutlinedInput-root': {
                  backgroundColor: 'rgba(255, 255, 255, 0.1)',
                  backdropFilter: 'blur(10px)',
                  color: '#ffffff',
                  '& fieldset': {
                    borderColor: 'rgba(255, 255, 255, 0.2)',
                  },
                  '&:hover fieldset': {
                    borderColor: 'rgba(255, 255, 255, 0.3)',
                  },
                  '&.Mui-focused fieldset': {
                    borderColor: '#ffffff',
                  },
                },
                '& .MuiInputLabel-root': {
                  color: 'rgba(255, 255, 255, 0.7)',
                },
                '& .MuiInputBase-input': {
                  color: '#ffffff',
                  '&::placeholder': {
                    color: 'rgba(255, 255, 255, 0.5)',
                  },
                },
              }}
            >
              <TextField
                size="small"
                placeholder="Enter your email"
                fullWidth
                sx={{ flex: 1 }}
              />
              <Button
                variant="contained"
                sx={{
                  backgroundColor: 'rgba(255, 255, 255, 0.2)',
                  backdropFilter: 'blur(10px)',
                  '&:hover': {
                    backgroundColor: 'rgba(255, 255, 255, 0.3)',
                    transform: 'translateY(-2px)',
                  },
                  transition: 'all 0.3s ease-in-out',
                  minWidth: 'auto',
                  px: 2,
                }}
              >
                <Send />
              </Button>
            </Box>
          </Grid>
          <Grid item xs={12} sm={4}>
            <Typography 
              variant="h6" 
              gutterBottom 
              sx={{ 
                color: '#ffffff',
                fontWeight: 700,
                fontSize: '1.25rem',
                letterSpacing: '0.5px',
                mb: 3,
                position: 'relative',
                '&::after': {
                  content: '""',
                  position: 'absolute',
                  bottom: -8,
                  left: 0,
                  width: '40px',
                  height: '3px',
                  background: 'linear-gradient(90deg, #64b5f6, transparent)',
                  borderRadius: '2px',
                }
              }}
            >
              Quick Links
            </Typography>
            <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1.5 }}>
              {['Home', 'Catalog', 'About Us', 'Contact'].map((item) => (
                <Link
                  key={item}
                  href={`/${item.toLowerCase().replace(' ', '-')}`}
                  color="inherit"
                  sx={{
                    color: 'rgba(255, 255, 255, 0.9)',
                    textDecoration: 'none',
                    fontSize: '0.95rem',
                    transition: 'all 0.2s ease-in-out',
                    '&:hover': {
                      color: '#ffffff',
                      transform: 'translateX(5px)',
                      textDecoration: 'none',
                    },
                    '&::before': {
                      content: '"→"',
                      marginRight: '8px',
                      opacity: 0,
                      transition: 'all 0.2s ease-in-out',
                    },
                    '&:hover::before': {
                      opacity: 1,
                    }
                  }}
                >
                  {item}
                </Link>
              ))}
            </Box>
          </Grid>
          <Grid item xs={12} sm={4}>
            <Typography 
              variant="h6" 
              gutterBottom 
              sx={{ 
                color: '#ffffff',
                fontWeight: 700,
                fontSize: '1.25rem',
                letterSpacing: '0.5px',
                mb: 3,
                position: 'relative',
                '&::after': {
                  content: '""',
                  position: 'absolute',
                  bottom: -8,
                  left: 0,
                  width: '40px',
                  height: '3px',
                  background: 'linear-gradient(90deg, #64b5f6, transparent)',
                  borderRadius: '2px',
                }
              }}
            >
              Connect With Us
            </Typography>
            <Box sx={{ display: 'flex', gap: 2, mb: 3 }}>
              {[
                { Icon: Facebook, label: 'Facebook', color: '#1877F2' },
                { Icon: Twitter, label: 'Twitter', color: '#1DA1F2' },
                { Icon: Instagram, label: 'Instagram', color: '#E4405F' },
                { Icon: LinkedIn, label: 'LinkedIn', color: '#0A66C2' }
              ].map(({ Icon, label, color }) => (
                <IconButton
                  key={label}
                  color="inherit"
                  aria-label={label}
                  sx={{
                    backgroundColor: 'rgba(255, 255, 255, 0.1)',
                    backdropFilter: 'blur(10px)',
                    '&:hover': {
                      backgroundColor: color,
                      transform: 'translateY(-3px)',
                      boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
                    },
                    transition: 'all 0.3s ease-in-out',
                  }}
                >
                  <Icon />
                </IconButton>
              ))}
            </Box>
            <Typography 
              variant="body2" 
              sx={{ 
                color: 'rgba(255, 255, 255, 0.9)',
                lineHeight: 1.7,
                fontSize: '0.95rem'
              }}
            >
              Follow us on social media for the latest updates and exclusive offers.
            </Typography>
          </Grid>
        </Grid>
        <Box 
          sx={{ 
            mt: 6, 
            pt: 3, 
            borderTop: '1px solid rgba(255, 255, 255, 0.1)',
            position: 'relative',
            '&::before': {
              content: '""',
              position: 'absolute',
              top: -1,
              left: '50%',
              transform: 'translateX(-50%)',
              width: '100px',
              height: '1px',
              background: 'linear-gradient(90deg, transparent, rgba(255, 255, 255, 0.3), transparent)',
            }
          }}
        >
          <Typography 
            variant="body2" 
            align="center" 
            sx={{ 
              color: 'rgba(255, 255, 255, 0.8)',
              fontSize: '0.9rem',
              letterSpacing: '0.5px'
            }}
          >
            © {new Date().getFullYear()} SimpleMarket. All rights reserved.
          </Typography>
        </Box>
      </Container>
    </Box>
  );
};

export default Footer;
