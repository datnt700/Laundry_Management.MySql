function TopMenu() {
    const classes = useStyles();
  
    return (
      <AppBar position='fixed' className={classes.appBar}>
        <Toolbar>
          <IconButton
            edge='start'
            className={classes.menuButton}
            color='inherit'
            aria-label='menu'
          >
            <MenuIcon />
          </IconButton>
          <MenuItem>
            <Typography variant='h6' className={classes.title}>
              Home
            </Typography>
          </MenuItem>
          <MenuItem>
            <Typography variant='h6' className={classes.title}>
              About
            </Typography>
          </MenuItem>
        </Toolbar>
      </AppBar>
    );
  }
  
  export default TopMenu;