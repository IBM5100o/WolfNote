This directory contains a simple static front-end for the Grid Counter app.

- `index.html` - main page
- `styles.css` - styling
- `app.js` - behavior (left click: -1, right click: +1)

To preview locally: open `web/index.html` in your browser.

To publish: push to `main`; GitHub Actions will deploy `web/` to `gh-pages` branch using the workflow at `.github/workflows/gh-pages.yml`.
