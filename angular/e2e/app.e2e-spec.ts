import { eConLabTemplatePage } from './app.po';

describe('eConLab App', function() {
  let page: eConLabTemplatePage;

  beforeEach(() => {
    page = new eConLabTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
