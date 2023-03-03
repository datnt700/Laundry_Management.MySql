// Styles and imports were ommited
function MainContent() {
    const classes = useStyles();
  
    return (
      <main className={classes.fullWidth}>
        <div className={classes.toolbar} />
        <div className={classes.title}>
          <Typography variant='h6'>Title</Typography>
        </div>
        <div className={classes.content}>
          <Typography paragraph>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc maximus,
            nulla ut commodo sagittis, sapien dui mattis dui, non pulvinar lorem
            felis nec erat
          </Typography>
        </div>
      </main>
    );
  }
  
  export default MainContent;