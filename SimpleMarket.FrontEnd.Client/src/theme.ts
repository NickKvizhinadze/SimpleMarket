import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2', // Deeper blue
      light: '#42a5f5',
      dark: '#1565c0',
      contrastText: '#ffffff',
    },
    secondary: {
      main: '#2196f3', // Bright blue
      light: '#64b5f6',
      dark: '#1976d2',
      contrastText: '#ffffff',
    },
    background: {
      default: '#e3f2fd', // Light blue background
      paper: '#ffffff',
    },
    text: {
      primary: '#1a237e', // Deep blue text
      secondary: '#283593', // Medium blue text
    },
    divider: 'rgba(25, 118, 210, 0.12)',
  },
  typography: {
    fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontWeight: 800,
      letterSpacing: '-0.02em',
    },
    h2: {
      fontWeight: 700,
      letterSpacing: '-0.01em',
    },
    h3: {
      fontWeight: 600,
    },
    h4: {
      fontWeight: 600,
    },
    h5: {
      fontWeight: 600,
    },
    h6: {
      fontWeight: 600,
    },
    button: {
      fontWeight: 500,
      letterSpacing: '0.01em',
    },
  },
  components: {
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundColor: '#1976d2',
          backgroundImage: 'linear-gradient(45deg, #1976d2 0%, #2196f3 100%)',
          boxShadow: '0 2px 4px rgba(25, 118, 210, 0.2)',
          '& .MuiTypography-root': {
            color: '#ffffff',
          },
          '& .MuiButton-root': {
            color: '#ffffff',
            '&:hover': {
              backgroundColor: 'rgba(255, 255, 255, 0.1)',
            },
          },
          '& .MuiIconButton-root': {
            color: '#ffffff',
            '&:hover': {
              backgroundColor: 'rgba(255, 255, 255, 0.1)',
            },
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          boxShadow: '0 4px 20px rgba(25, 118, 210, 0.08)',
          borderRadius: '16px',
          transition: 'all 0.3s ease-in-out',
          backgroundImage: 'linear-gradient(180deg, #ffffff 0%, #e3f2fd 100%)',
          '&:hover': {
            transform: 'translateY(-4px)',
            boxShadow: '0 12px 28px rgba(25, 118, 210, 0.12)',
          },
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: '12px',
          textTransform: 'none',
          fontWeight: 500,
          padding: '10px 24px',
        },
        contained: {
          backgroundImage: 'linear-gradient(45deg, #1976d2 0%, #2196f3 100%)',
          boxShadow: '0 4px 12px rgba(25, 118, 210, 0.2)',
          '&:hover': {
            backgroundImage: 'linear-gradient(45deg, #1565c0 0%, #1976d2 100%)',
            boxShadow: '0 6px 16px rgba(25, 118, 210, 0.3)',
          },
        },
        outlined: {
          borderWidth: '2px',
          '&:hover': {
            borderWidth: '2px',
            backgroundImage: 'linear-gradient(45deg, rgba(25, 118, 210, 0.1) 0%, rgba(33, 150, 243, 0.05) 100%)',
          },
        },
      },
    },
    MuiContainer: {
      styleOverrides: {
        root: {
          paddingLeft: '24px',
          paddingRight: '24px',
        },
      },
    },
    MuiCardMedia: {
      styleOverrides: {
        root: {
          transition: 'transform 0.5s ease-in-out',
        },
      },
    },
    MuiIconButton: {
      styleOverrides: {
        root: {
          transition: 'all 0.2s ease-in-out',
          '&:hover': {
            transform: 'scale(1.1)',
            backgroundImage: 'linear-gradient(45deg, rgba(25, 118, 210, 0.1) 0%, rgba(33, 150, 243, 0.05) 100%)',
          },
        },
      },
    },
  },
  shape: {
    borderRadius: 12,
  },
});

export default theme; 
