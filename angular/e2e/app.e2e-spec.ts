import { AycProjectBudgetingTemplatePage } from './app.po';

describe('AycProjectBudgeting App', function() {
  let page: AycProjectBudgetingTemplatePage;

  beforeEach(() => {
    page = new AycProjectBudgetingTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
