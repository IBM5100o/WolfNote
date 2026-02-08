const gridEl = document.getElementById('grid');
const countInput = document.getElementById('count');
const applyBtn = document.getElementById('apply');

function buildGrid(count) {
  gridEl.innerHTML = '';
  for (let i = 0; i < count; i++) {
    const cell = document.createElement('div');
    cell.className = 'cell';
    cell.tabIndex = 0;
    cell.textContent = '0';

    cell.addEventListener('click', (e) => {
      // left click
      const v = parseInt(cell.textContent || '0', 10);
      cell.textContent = (v - 1).toString();
    });

    cell.addEventListener('contextmenu', (e) => {
      e.preventDefault();
      const v = parseInt(cell.textContent || '0', 10);
      cell.textContent = (v + 1).toString();
    });

    gridEl.appendChild(cell);
  }
}

applyBtn.addEventListener('click', () => buildGrid(Number(countInput.value)));

// keyboard support: Enter decreases, Shift+Enter increases
gridEl.addEventListener('keydown', (e) => {
  const target = e.target;
  if (!target || !target.classList.contains('cell')) return;
  if (e.key === 'Enter' && !e.shiftKey) {
    target.textContent = (parseInt(target.textContent || '0', 10) - 1).toString();
  } else if (e.key === 'Enter' && e.shiftKey) {
    target.textContent = (parseInt(target.textContent || '0', 10) + 1).toString();
  }
});

// build initial
buildGrid(Number(countInput.value));
