const gridEl = document.getElementById('grid');
const countInput = document.getElementById('count');
const applyBtn = document.getElementById('apply');
const namesTextarea = document.getElementById('names');
const setNamesBtn = document.getElementById('setNames');

let playerNames = [];

function buildGrid(count) {
  gridEl.innerHTML = '';
  for (let i = 0; i < count; i++) {
    const cell = document.createElement('div');
    cell.className = 'cell';
    cell.tabIndex = 0;

    const nameEl = document.createElement('div');
    nameEl.className = 'name';
    const numEl = document.createElement('div');
    numEl.className = 'number';
    numEl.textContent = '0';

    if (playerNames.length !== 0 && i < playerNames.length) {
      nameEl.textContent = playerNames[i];
    } else {
      nameEl.textContent = '';
    }

    // left click decreases, right click increases
    cell.addEventListener('click', (e) => {
      const v = parseInt(numEl.textContent || '0', 10);
      numEl.textContent = (v - 1).toString();
    });

    cell.addEventListener('contextmenu', (e) => {
      e.preventDefault();
      const v = parseInt(numEl.textContent || '0', 10);
      numEl.textContent = (v + 1).toString();
    });

    cell.appendChild(nameEl);
    cell.appendChild(numEl);
    gridEl.appendChild(cell);
  }
}

applyBtn.addEventListener('click', () => buildGrid(Number(countInput.value)));

setNamesBtn.addEventListener('click', () => {
  // parse similarly to the C# logic: split on tabs/newlines, look for parts starting with '¡»'
  const text = namesTextarea.value || '';
  const parts = text.split(/\t|\r?\n/).map(s => s.trim()).filter(s => s.length > 0);
  playerNames = [];
  let flag = 0;
  for (const part of parts) {
    if (part[0] === '◆') {
      if (flag === 0) {
        playerNames.push(part.substring(1));
        flag = 1;
      } else {
        flag = 0;
      }
    } else {
      flag = 0;
    }
  }
  buildGrid(Number(countInput.value));
  namesTextarea.value = '';
  playerNames = [];
});

// build initial
buildGrid(Number(countInput.value));
