import { AppBar, Toolbar, Typography, Button, IconButton, Badge, Box, useMediaQuery, useTheme, Container } from '@mui/material';
import { ShoppingCart, Person, Menu, Search } from '@mui/icons-material';
import { Link as RouterLink } from 'react-router-dom';
import { useState } from 'react';
import logo from '../assets/logo.png';

const Navbar = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  return (
    <AppBar 
      position="sticky" 
      elevation={0}
      sx={{ 
        backgroundColor: 'background.paper',
        borderBottom: '1px solid',
        borderColor: 'divider'
      }}
    >
      <Container maxWidth="xl">
        <Toolbar sx={{ px: { xs: 0 }, minHeight: { xs: 64, md: 70 } }}>
          <Box
            component={RouterLink}
            to="/"
            sx={{ 
              flexGrow: 1, 
              textDecoration: 'none',
              display: 'flex',
              alignItems: 'center',
              '&:hover': {
                '& img': {
                  transform: 'scale(1.05)',
                  filter: 'brightness(1.1)',
                }
              }
            }}
          >
            <Box
              component="img"
              src={logo}
              alt="SimpleMarket Logo"
              sx={{
                height: '40px',
                transition: 'all 0.3s ease-in-out',
              }}
            />
          </Box>
          
          {isMobile ? (
            <IconButton
              color="primary"
              onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
              sx={{
                '&:hover': {
                  backgroundColor: 'rgba(25, 118, 210, 0.04)'
                }
              }}
            >
              <Menu />
            </IconButton>
          ) : (
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
              <Button 
                color="primary" 
                component={RouterLink} 
                to="/catalog"
                sx={{
                  fontWeight: 500,
                  fontSize: '0.95rem',
                  '&:hover': {
                    backgroundColor: 'rgba(25, 118, 210, 0.04)'
                  }
                }}
              >
                Catalog
              </Button>
              <Button 
                color="primary" 
                component={RouterLink} 
                to="/contact"
                sx={{
                  fontWeight: 500,
                  fontSize: '0.95rem',
                  '&:hover': {
                    backgroundColor: 'rgba(25, 118, 210, 0.04)'
                  }
                }}
              >
                Contact
              </Button>
              <IconButton 
                color="primary" 
                component={RouterLink} 
                to="/search"
                sx={{
                  '&:hover': {
                    backgroundColor: 'rgba(25, 118, 210, 0.04)'
                  }
                }}
              >
                <Search />
              </IconButton>
              <IconButton 
                color="primary" 
                component={RouterLink} 
                to="/cart"
                sx={{
                  '&:hover': {
                    backgroundColor: 'rgba(25, 118, 210, 0.04)'
                  }
                }}
              >
                <Badge badgeContent={0} color="secondary">
                  <ShoppingCart />
                </Badge>
              </IconButton>
              <IconButton 
                color="primary" 
                component={RouterLink} 
                to="/login"
                sx={{
                  '&:hover': {
                    backgroundColor: 'rgba(25, 118, 210, 0.04)'
                  }
                }}
              >
                <Person />
              </IconButton>
            </Box>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default Navbar; 
